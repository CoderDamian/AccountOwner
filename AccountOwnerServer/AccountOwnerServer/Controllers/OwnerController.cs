using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using Persistence.Repositories;
using Persistence.Contracts;
using Entities.Models;
using AutoMapper;
using LoggerService.Contracts;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AccountOwnerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
                IEnumerable<Account> accounts = _repository.AccountRepository.AccountsByOwner(id);

                accounts = _repository.AccountRepository.FindByCondition(p=>p.ID.Equals(id));

                if (accounts.Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");

                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }

                _repository.OwnerRepository.Delete(owner);

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
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Owner> owners = _repository.OwnerRepository.GetAllOwners();

                IEnumerable<OwnerDto> ownersDTO = _mapper.Map<IEnumerable<OwnerDto>>(owners);

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

                _repository.OwnerRepository.Create(newOwner);

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

                _repository.OwnerRepository.Update(owner);

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