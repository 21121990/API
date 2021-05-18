using Api.Data.Collections;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;

namespace Api.Controllers

{
    [ApiController]
    [Route("[controller]")]

    public class InfectadoController : ControllerBase
    {

        MongoDB _mongoDB;
        IMongoCollection<infectado> _infectadosCollection;


        public InfectadoController(MongoDB mongodb)
        {
            _mongoDB = mongodb;
            _infectadosCollection = _mongoDB.DB.GetCollection<infectado>(typeof(infectado).Name.ToLower());

        }
        [HttpPost]

        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectadoo = new infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadosCollection.InsertOne(infectadoo);

            return StatusCode(201, "Infectado adicionado com sucesso");

        }
        [HttpGet]

        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<infectado>.Filter.Empty).ToList();
            return Ok(infectados);
        }
    }
}