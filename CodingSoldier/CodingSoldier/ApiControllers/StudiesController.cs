using System.Web.Http;
using System.Web.Http.Description;
using CodingSoldier.Core.Models;
using CodingSoldier.Core.UnitOfWork;
using AutoMapper;
using System.Collections.Generic;
using CodingSoldier.Core.Entities;

namespace CodingSoldier.ApiControllers
{
    [RoutePrefix("api")]
    public class StudiesController : BaseApiController<Study, StudyApiEntity>
    {
        public StudiesController(IUnitOfWork uow) : base(uow)
        {

        }

        // GET: api/Studies
        [AllowAnonymous]
        public List<StudyApiEntity> GetStudies()
        {
            return GetAll();
        }

        // GET: api/Studies/5
        [ResponseType(typeof(StudyApiEntity))]
        [AllowAnonymous]
        public IHttpActionResult GetStudy(int id)
        {
            return Get(id);
        }

        // PUT: api/Studies/5
        [ResponseType(typeof(void))]
        [Route("updatestudy")]
        [HttpPost]
        public IHttpActionResult PutStudy(StudyApiEntity study)
        {
            var model = Mapper.Map<Study>(study);
            return UpdateModel(model);
        }

        // POST: api/Studies
        [ResponseType(typeof(StudyApiEntity))]
        [Route("addstudy")]
        public IHttpActionResult PostStudy(StudyApiEntity study)
        {
            var model = Mapper.Map<Study>(study);
            return PostModel(model);
        }

        // DELETE: api/Studies/5
        [ResponseType(typeof(StudyApiEntity))]
        public IHttpActionResult DeleteStudy(int id)
        {
            return DeleteModel(id);
        }       
    }
}