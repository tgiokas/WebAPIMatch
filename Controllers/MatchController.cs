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
    public class MatchController : ControllerBase
    {
        private readonly IServiceMatch matchService;
        
        public MatchController(IServiceMatch service)
        {
            matchService = service;
        }

        [HttpGet]
        [Route("[action]")] 
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTOMatch))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]

        public IEnumerable<DTOMatch> Get()
        {
            return matchService.GetMatches();
        }

        [HttpGet]       
        [Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTOMatch))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]

        public IActionResult Get(int id)
        {        
            var matchDTO= matchService.GetMatchById(id);
            if (matchDTO == null)
            {
                return NotFound();
            }

            return Ok(matchDTO);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]            
        [SwaggerRequestExample(typeof(SwaggerRequestMatch), typeof(SwaggerExampleMatch))]   
        
        public IActionResult Add(DTOMatch matchDTO)
        {
            try
            {
                if (matchDTO == null)
                {
                    return BadRequest();
                }

                var match = matchService.AddMatch(matchDTO);
                if (match == null)
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
        [SwaggerRequestExample(typeof(SwaggerRequestMatch), typeof(SwaggerExampleMatch))]

        public IActionResult Edit(int id, DTOMatch matchDTO)
        {
            try
            {
                if (matchDTO == null)
                {
                    return BadRequest();
                }
                
                var matchDTOcheck = matchService.GetMatchById(id);
                if (matchDTOcheck == null)
                {
                    return NotFound();
                }

                var match = matchService.UpdateMatch(id, matchDTO);
                if (match == null)
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

        public IActionResult Delete(int id)
        {
            try
            {       
                var match = matchService.DeleteMatch(id);
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

