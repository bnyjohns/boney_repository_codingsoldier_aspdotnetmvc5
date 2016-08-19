using CodingSoldier.Core.Models;
using CodingSoldier.Core.UnitOfWork;
using CodingSoldier.Models;

namespace CodingSoldier.Controllers
{
    public class StudiesController : BaseController<Study, StudyViewModel>
    {
        public StudiesController(IUnitOfWork uow) :
            base(uow)
        {

        }        
    }
}