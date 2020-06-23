using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OriginBanking.Data.DTOs;
using OriginBanking.Data.Repositories.RepositoryCard;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OriginBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository db;

        public CardsController(ICardRepository db)
        {
            this.db = db;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{cardnumber}")]
        public UserDTO GetUser(string cardnumber)
        {
            return db.GetUser(cardnumber);
        }

        [HttpPost]
        [Route("ExistAndIsNotBlocked")]
        public ActionResult<CardDTO> CardNumber([FromBody]CardDTO model)
        {
            if (db.ExistAndIsNotBlocked(model))
                return Ok();

            return BadRequest("No existe el numero de tarjeta o se encuentra bloqueada");
        }

        [HttpPost]
        [Route("EnterPin")]
        public IActionResult Pin([FromBody]CardDTO model)
        {
            if (db.PinIsCorrect(model))
                return BuildToken();

            if (model.Attempts != 0)
                return BadRequest(String.Format("Pin incorrecto te queda {0} intentos.", model.Attempts));

            db.BlockCard(model);
            return BadRequest("Tu Cuenta ha sido bloqueda");

        }


        private IActionResult BuildToken()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("llavesecretaqueparafacilitarsupruebaestahardcodeadaestonosedebehacer"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "OriginBanking",
               audience: "OriginBanking",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            });

        }
    }
}