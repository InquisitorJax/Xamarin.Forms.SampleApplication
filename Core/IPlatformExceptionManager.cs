using System.Threading.Tasks;

namespace SampleApplication.Core
{
    public interface IPlatformExceptionManager
    {
        Task ReportApplicationCrash();
    }
}