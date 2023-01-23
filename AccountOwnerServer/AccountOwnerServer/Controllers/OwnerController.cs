using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using LoggerService.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace AccountOwnerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._linkGenerator = linkGenerator;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Owner? owner = _repository.OwnerRepository.GetOwnerById(id);

            if (owner is null)
            {
                _logger.LogError($"Owner with id: {id}, hasn't been found in db.");

                return NotFound();
            }

            try
            {
                IEnumerable<Account> accounts = _repository.AccountRepository.GetAccountsByOwner(id);

                if (accounts.Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");

                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }

                _repository.OwnerRepository.DeleteOwner(owner);

                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery]OwnerParameters ownerParameter)
        {
            if (!ownerParameter.ValidYearRange)
                return BadRequest("Max year of birth cannot be less than min year of birth");

            try
            {
                PagedList<Owner> owners = _repository.OwnerRepository.GetOwners(ownerParameter);

                var metadata = new
                {
                    owners.TotalCount,
                    owners.PageSize,
                    owners.CurrentPage,
                    owners.TotalPages,
                    owners.HasNext,
                    owners.HasPrevious
                };

                IEnumerable<OwnerDto> ownersDTO = _mapper.Map<IEnumerable<OwnerDto>>(owners);

                for (var index = 0; index < owners.Count(); index++)
                {
                    var ownerLinks = CreateLinksForOwner(owners[index].ID);
                    owners[index].Add("Links", ownerLinks);
                }

                _logger.LogInfo("Returning all owners from the database.");

                return Ok(ownersDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred {ex.Message}");

                return StatusCode(500, "Internal server error");

                throw;
            }
        }

        private object CreateLinksForOwner(object id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{ownerID}", Name = "OwnerById")]
        public IActionResult GetByID(string ownerID)
        {
            try
            {
                Owner? owner = _repository.OwnerRepository.GetOwnerById(ownerID);

                if (owner is null)
                {
                    _logger.LogError($"Owner with id: {ownerID}, hasn't been found in db.");

                    return NotFound();
                }


                _logger.LogInfo("Returning an specific owner");

                OwnerDto ownerDto = _mapper.Map<OwnerDto>(owner);

                return Ok(ownerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred {ex.Message}");

                return StatusCode(500, "Internal server error");

                throw;
            }
        }

        [HttpPost]
        public IActionResult Save([FromBody] OwnerForCreationDto creationDto)
        {
            try
            {
                if (creationDto is null)
                {
                    _logger.LogError("Owner object sent from client is null");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client");
                    return BadRequest("Invalid model object");
                }

                Owner newOwner = _mapper.Map<Owner>(creationDto);

                _repository.OwnerRepository.CreateOwner(newOwner);

                _repository.Save();

                OwnerDto ownerDto = _mapper.Map<OwnerDto>(newOwner);

                return CreatedAtRoute("OwnerById", new { id = ownerDto.Id }, ownerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] OwnerForUpdateDto updateDto)
        {
            if (updateDto is null)
            {
                _logger.LogError("Owner object sent from client is null.");

                return BadRequest("Owner object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid owner object sent from client.");

                return BadRequest("Invalid model object");
            }

            try
            {
                Owner? owner = _repository.OwnerRepository.GetOwnerById(id);

                if (owner is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");

                    return NotFound();
                }
                owner = _mapper.Map<Owner>(updateDto);

                _repository.OwnerRepository.UpdateOwner(owner);

                _repository.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError("x");

                return StatusCode(500, "Internal server error");
            }
        }
    }
}