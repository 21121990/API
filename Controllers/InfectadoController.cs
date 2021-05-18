using Api.Data.Collections;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers

{
    [ApiController]
    [Route("[controller]")]

    public class InfectadoController : ControllerBase
    {

        Data.MongoDB _mongoDB;
        IMongoCollection<infectado> _infectadosCollection;


        public InfectadoController(Data.MongoDB mongodb)
        {
            _mongoDB = mongodb;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(infectado).Name.ToLower());

        }
        [HttpPost]

        public ActionResult SalvarInfectado([Frombody] infctadpDto dto)
        {
            var infectado = new infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadosCollection.insertOne(infectado);

            return StatusCode(201, "Infectado adicionado com sucesso");

        }
        [httpGet]

        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<infectado>.Filter.Empty).ToList();
            return OK(infectados);

        }
    }
}