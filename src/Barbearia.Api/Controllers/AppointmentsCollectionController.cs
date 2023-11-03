using Barbearia.Application.Features.AppointmentsCollection;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Barbearia.Api.Controllers
{
    [ApiController]
    [Route("api/appointmentscollection")]
    public class AppointmentsCollectionController : MainController
    {
        private readonly IMediator _mediator;

        public AppointmentsCollectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAppointmentsCollectionQueryResponse>>> GetAppointmentsCollection(
            string? searchQuery = "", int pageNumber = 1, int pageSize = 5)
        {
            var getAppointmentsCollectionQuery = new GetAppointmentsCollectionQuery
            {
                SearchQuery = searchQuery,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var getAppointmentsCollectionQueryResponse = await _mediator.Send(getAppointmentsCollectionQuery);

            if (!getAppointmentsCollectionQueryResponse.IsSuccess)
                return HandleRequestError(getAppointmentsCollectionQueryResponse);

            var appointmentsToReturn = getAppointmentsCollectionQueryResponse.Appointments;
            var paginationMetadata = getAppointmentsCollectionQueryResponse.PaginationMetadata;

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(appointmentsToReturn);
        }
    }
}
