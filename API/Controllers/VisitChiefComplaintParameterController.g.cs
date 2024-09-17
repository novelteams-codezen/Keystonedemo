using Microsoft.AspNetCore.Mvc;
using Keystonedemo.Models;
using Keystonedemo.Services;
using Keystonedemo.Entities;
using Keystonedemo.Filter;
using Keystonedemo.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace Keystonedemo.Controllers
{
    /// <summary>
    /// Controller responsible for managing visitchiefcomplaintparameter related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting visitchiefcomplaintparameter information.
    /// </remarks>
    [Route("api/visitchiefcomplaintparameter")]
    [Authorize]
    public class VisitChiefComplaintParameterController : ControllerBase
    {
        private readonly IVisitChiefComplaintParameterService _visitChiefComplaintParameterService;

        /// <summary>
        /// Initializes a new instance of the VisitChiefComplaintParameterController class with the specified context.
        /// </summary>
        /// <param name="ivisitchiefcomplaintparameterservice">The ivisitchiefcomplaintparameterservice to be used by the controller.</param>
        public VisitChiefComplaintParameterController(IVisitChiefComplaintParameterService ivisitchiefcomplaintparameterservice)
        {
            _visitChiefComplaintParameterService = ivisitchiefcomplaintparameterservice;
        }

        /// <summary>Adds a new visitchiefcomplaintparameter</summary>
        /// <param name="model">The visitchiefcomplaintparameter data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("VisitChiefComplaintParameter",Entitlements.Create)]
        public IActionResult Post([FromBody] VisitChiefComplaintParameter model)
        {
            var id = _visitChiefComplaintParameterService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of visitchiefcomplaintparameters based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of visitchiefcomplaintparameters</returns>
        [HttpGet]
        [UserAuthorize("VisitChiefComplaintParameter",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult Get([FromQuery] string filters, string searchTerm, int pageNumber = 1, int pageSize = 10, string sortField = null, string sortOrder = "asc")
        {
            List<FilterCriteria> filterCriteria = null;
            if (pageSize < 1)
            {
                return BadRequest("Page size invalid.");
            }

            if (pageNumber < 1)
            {
                return BadRequest("Page mumber invalid.");
            }

            if (!string.IsNullOrEmpty(filters))
            {
                filterCriteria = JsonHelper.Deserialize<List<FilterCriteria>>(filters);
            }

            var result = _visitChiefComplaintParameterService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific visitchiefcomplaintparameter by its primary key</summary>
        /// <param name="id">The primary key of the visitchiefcomplaintparameter</param>
        /// <returns>The visitchiefcomplaintparameter data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("VisitChiefComplaintParameter",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _visitChiefComplaintParameterService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific visitchiefcomplaintparameter by its primary key</summary>
        /// <param name="id">The primary key of the visitchiefcomplaintparameter</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("VisitChiefComplaintParameter",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _visitChiefComplaintParameterService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific visitchiefcomplaintparameter by its primary key</summary>
        /// <param name="id">The primary key of the visitchiefcomplaintparameter</param>
        /// <param name="updatedEntity">The visitchiefcomplaintparameter data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("VisitChiefComplaintParameter",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] VisitChiefComplaintParameter updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _visitChiefComplaintParameterService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific visitchiefcomplaintparameter by its primary key</summary>
        /// <param name="id">The primary key of the visitchiefcomplaintparameter</param>
        /// <param name="updatedEntity">The visitchiefcomplaintparameter data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("VisitChiefComplaintParameter",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<VisitChiefComplaintParameter> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _visitChiefComplaintParameterService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}