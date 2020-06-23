using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OriginBanking.Data.DTOs;
using OriginBanking.Data.Repositories.RepositoryBalance;
using System.Collections.Generic;

namespace OriginBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceRepository db;

        public BalanceController(IBalanceRepository db)
        {
            this.db = db;
        }

        [HttpGet("{cardnumber}")]
        public IEnumerable<BalanceDTO> GetBalance(string cardnumber)
        {
            return db.GetBalance(cardnumber);
        }

        [HttpPost]
        [Route("GetMoney")]
        public ActionResult<CardDTO> GetMoney([FromBody]CardDTO model)
        {
            if (db.OverPassBalance(model))
                return BadRequest("El monto ingresado es superiror al balance existente");

            if (db.GetMoney(model))
                return Ok();

            return BadRequest("Ha ocurrido un error");
        }


    }
}