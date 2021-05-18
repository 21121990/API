using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Data.Collections
{
    public class infectado
    {
        public infectado(DateTime dataNascimento, GeoJson2DGeographicCoordinates localizacao, string sexo, double latitude, double longitude)
        {
            this.DataNascimento = dataNascimento;
            this.Localizacao =  new GeoJson2DGeographicCoordinates(longitude, latitude);
            this.Sexo = sexo;

        }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }
    }
}