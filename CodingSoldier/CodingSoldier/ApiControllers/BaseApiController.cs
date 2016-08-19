using AutoMapper;
using CodingSoldier.Core.Entities;
using CodingSoldier.Core.Models;
using CodingSoldier.Core.Repository;
using CodingSoldier.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;

namespace CodingSoldier.ApiControllers
{
    [Authorize]
    public abstract class BaseApiController<TModel, TEntity> : ApiController
                                                               where TModel : class, IModel
                                                               where TEntity: IApiEntity
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IRepository<TModel> _modelRepository;

        public BaseApiController(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException(nameof(uow));
            _uow = uow;

            _modelRepository = _uow.Repository<TModel>();
        }

        protected virtual List<TEntity> GetAll()
        {
            return Mapper.Map<List<TEntity>>(_modelRepository.GetAll());
        }

        protected virtual IHttpActionResult Get(int id)
        {
            var model = _modelRepository.Get(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            var modelEntity = Mapper.Map<TEntity>(model);
            return Ok(modelEntity);
        }

        protected virtual IHttpActionResult UpdateModel(TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id == 0)
            {
                return BadRequest();
            }

            try
            {                
                _modelRepository.Update(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected virtual IHttpActionResult PostModel(TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            _modelRepository.Add(model);
            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        protected virtual IHttpActionResult DeleteModel(int id)
        {
            var post = _modelRepository.Get(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            _modelRepository.Remove(p => p.Id == id);
            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            _uow.Dispose();
            base.Dispose(disposing);
        }

        private bool ModelExists(int id)
        {
            return _modelRepository.Get(m => m.Id == id) != null;
        }
    }
}