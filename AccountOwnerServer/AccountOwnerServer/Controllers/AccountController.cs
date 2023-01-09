using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using AutoMapper;
using LoggerService.Contracts;
using Persistence.Contracts;

namespace AccountOwnerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public AccountController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
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
                IEnumerable<Account> accounts = _repository.AccountRepository.GetAllAccounts();

                IEnumerable<AccountDTO> accountssDTO = _mapper.Map<IEnumerable<AccountDTO>>(accounts);

                _logger.LogInfo("Returning all owners from the database.");

                return Ok(accountssDTO);
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
