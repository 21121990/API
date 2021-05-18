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
        [HttpPut]
        public ActionResult AtualizarInfectado([FromBody] InfectadoDto dto)
        {

            var infectados = new infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
           _infectadosCollection.UpdateOne(Builders<infectado>.Filter.Where(x => x.DataNascimento == dto.DataNascimento),Builders<infectado>.Update.Set("sexo", dto.Sexo));
            return Ok("Atualizado com sucesso");
        }

        [HttpDelete("{dtn}")]
        public ActionResult Delete(DateTime dtn)
        {

           _infectadosCollection.DeleteOne(Builders<infectado>.Filter.Where(x => x.DataNascimento == dtn));
            return Ok("Cadastro excluído");
        }
    }
}