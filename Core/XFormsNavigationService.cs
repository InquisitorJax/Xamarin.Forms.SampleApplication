using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;

namespace SampleApplication
{
	public class XFormsNavigationService : INavigationService
	{
		private Page _currentPage;

		private Dictionary<Page, bool> _pageInfoList;

		private Stack<IView> _viewStack;

		public XFormsNavigationService()
		{
			_viewStack = new Stack<IView>();
			_pageInfoList = new Dictionary<Page, bool>();
		}

		private Page CurrentPage
		{
			get
			{
				if (_currentPage == null)
					_currentPage = Application.Current.MainPage;
				return _currentPage;
			}
		}

		public object Current
		{
			get {return _currentPage; }
		}

		private IView CurrentView
		{
			get
			{
				//NOTE: will be null if the application's MainPage shell (usually a NavigationPage instance)
				return CurrentPage as IView;
			}
		}			

		public async Task GoBack()
		{
			await PopCurrentPageAsync();
			if (_viewStack.Count > 0)
				_currentPage = (Page)_viewStack.Peek();
		}

		public async Task NavigateAsync(string destination, Dictionary<string, string> args = null, bool modal = false, bool forgetCurrentPage = false)
		{
			IView view = ResolveView(destination);

			await ShowViewAsync(view, modal, forgetCurrentPage); //Show View first so that showing doesn't wait for state initialization

			IViewModel viewModel = await ResolveViewModelAsync(destination, args);

			view.ViewModel = viewModel;
			((Page)view).BindingContext = viewModel;
		}

		public async Task ResumeAsync()
		{
			if (CurrentView != null)
			{
				await CurrentView.ViewModel.LoadStateAsync();
			}
		}

		public async Task SuspendAsync()
		{
			if (CurrentView != null)
			{
				await CurrentView.ViewModel.SaveStateAsync();
			}
		}

		private async Task PopCurrentPageAsync()
		{
			if (CurrentPage != null)
			{
				bool isModal = false;
				if (_pageInfoList.ContainsKey(_currentPage))
				{
					isModal = _pageInfoList[_currentPage];
				}

				//NOTE: pop view stack befire calling popping navigation on page to cater for Page_Disappearing check
				PopViewStack();

				if (isModal)
				{
					var page = await _currentPage.Navigation.PopModalAsync(true);
				}
				else
				{
					await _currentPage.Navigation.PopAsync(true);
				}
			}
		}

		private IView PopViewStack()
		{
			IView poppedView = null;

			if (_viewStack.Count > 0 && _viewStack.Peek () == CurrentView) 
			{
				poppedView = _viewStack.Pop();
				poppedView.ViewModel.Closing();
			}
			if (_pageInfoList.ContainsKey (_currentPage)) 
			{				
				_pageInfoList.Remove(_currentPage);			
			}

			return poppedView;
		}

		private IView ResolveView(string destination)
		{
			IView view = CC.IoC.ResolveKeyed<IView>(destination);

			return view;
		}

		private async Task<IViewModel> ResolveViewModelAsync(string destination, Dictionary<string, string> args = null)
		{
			IViewModel viewModel = CC.IoC.ResolveKeyed<IViewModel>(destination);
			await viewModel.InitializeAsync(args);
			return viewModel;
		}

		private async Task ShowViewAsync(IView view, bool modal, bool forgetCurrentPage)
		{			
			//NOTE: Add view to stack first, so disappearing when moving forward will not remove view from stack
			_viewStack.Push(view);
			_pageInfoList.Add ((Page)view, modal);

			if (CurrentPage == null) 
			{
				//first time navigation
				Application.Current.MainPage = new NavigationPage((Page)view);
			} 
			else 
			{
				var page = (Page)view;
				page.Disappearing += Page_Disappearing;
				if (modal)
				{
					await CurrentPage.Navigation.PushModalAsync ((Page)view);
				} 
				else 
				{
					var navigation = DependencyService.Get<INavigation> ();

					if (forgetCurrentPage) {
						CurrentPage.Navigation.InsertPageBefore ((Page)view, CurrentPage);
						await PopCurrentPageAsync ();
					} else {
						await CurrentPage.Navigation.PushAsync ((Page)view);
					}
				}
			}

			_currentPage = view as Page;

		}

		private void Page_Disappearing (object sender, EventArgs e)
		{
			IView view = sender as IView;

			if (view != null)
			{
				if (_viewStack.Count > 0 && _viewStack.Peek() == view) //ie: View has not been popped from local stack yet
				{
					//hard Back button was pressed, or back navigation on nav bar was pressed (ie: Navigate was not called on navigation service)
					PopViewStack ();
					if (_viewStack.Count > 0)
						_currentPage = (Page)_viewStack.Peek();
					else
					{
						//TODO: Log.Error: multiple validation requests for the same view
					}
				}
			}

		}
	}
}

