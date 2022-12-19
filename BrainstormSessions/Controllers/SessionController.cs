using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.ViewModels;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BrainstormSessions.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly ILog _log = LogManager.GetLogger(typeof(HomeController));

        public SessionController(IBrainstormSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index),
                    controllerName: "Home");
            }

            var session = await _sessionRepository.GetByIdAsync(id.Value);
            if (session == null)
            {
                return Content("Session not found.");
            }

            var viewModel = new StormSessionViewModel()
            {
                DateCreated = session.DateCreated,
                Name = session.Name,
                Id = session.Id
            };

            _log.Debug("View model returned");
            _log.Debug("Message 2");

            return View(viewModel);
        }
    }
}
