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

        [HttpGet("{ownerID}/account")]
        public IActionResult GetByID(string ownerID)
        {
            try
            {
                Owner? owner = _repository.OwnerRepository.GetOwnerWithDetails(ownerID);

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
    }
}