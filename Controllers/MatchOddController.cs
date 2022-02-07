using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

using WebAPIMatch.Models;
using WebAPIMatch.Service;
using WebAPIMatch.Helpers.Swagger;

namespace WebAPICore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchOddController : ControllerBase
    {
        private readonly IServiceMatchOdd matchOddService;
        
        public MatchOddController(IServiceMatchOdd service)
        {
            matchOddService = service;
        }

        [HttpPost]        
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(SwaggerRequestMatchOdd), typeof(SwaggerExampleMatchOdd))]

        public IActionResult Add(int matchId, DTOMatchOdd matchOddDTO)
        {
            try
            {
                if (matchOddDTO == null)
                {
                    return BadRequest();
                }

                var matchDTO = matchOddService.GetMatchById(matchId);
                if (matchDTO == null)
                {
                    return NotFound();
                }

                var matchOdd = matchOddService.AddMatchOdd(matchId,matchOddDTO);
                if (matchOdd == null)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception)
            {
                throw new Exception(HttpStatusCode.InternalServerError.ToString());
            }
        }

        [HttpPut]
        [Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(SwaggerRequestMatchOdd), typeof(SwaggerExampleMatchOdd))]

        public IActionResult Edit(int id, DTOMatchOdd matchOddDTO)
        {
            try
            {
                if (matchOddDTO == null)
                {
                    return BadRequest();
                }

                var matchOddcheck = matchOddService.GetMatchOddById(id);
                if (matchOddcheck == null)
                {
                    return NotFound();
                }

                var matchOdd = matchOddService.UpdateMatchOdd(id, matchOddDTO);
                if (matchOdd == null)
                {
                    return BadRequest();
                }

                return Ok();                 
            }
            catch (Exception)
            {
                throw new Exception(HttpStatusCode.InternalServerError.ToString());
            }           
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Delete(int id)
        {
            try
            {
                var match = matchOddService.DeleteMatchOdd(id);
                if (match == null)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception)
            {
                throw new Exception(HttpStatusCode.InternalServerError.ToString());
            }           
        }

    }
}

