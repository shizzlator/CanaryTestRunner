using System.Threading;
using System.Web.Mvc;
using CanaryTestRunner;

namespace Example.Controllers
{
    public class CanaryController : Controller
    {
        private readonly ITestRunner _canaryTestRunner;

        public CanaryController()
        {
            _canaryTestRunner = new TestRunner();
        }

        public ActionResult Index()
        {
            var canaryTestOutput = _canaryTestRunner.RunAllTests();
            return View(canaryTestOutput);
        }

    }
}
