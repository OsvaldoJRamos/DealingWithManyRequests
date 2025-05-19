using System.Text.Json.Serialization;

public class CepModel
{
    [JsonPropertyName("cep")]
    public string Cep { get; set; }
    [JsonPropertyName("logradouro")]
    public string Logradouro { get; set; }
    [JsonPropertyName("complemento")]
    public string Complemento { get; set; }
    [JsonPropertyName("bairro")]
    public string Bairro { get; set; }
    [JsonPropertyName("localidade")]
    public string Localidade { get; set; }
    [JsonPropertyName("uf")]
    public string Uf { get; set; }
    [JsonPropertyName("ibge")]
    public string Ibge { get; set; }
    [JsonPropertyName("gia")]
    public string Gia { get; set; }
    [JsonPropertyName("ddd")]
    public string Ddd { get; set; }
    [JsonPropertyName("siafi")]
    public string Siafi { get; set; }

    public override string ToString()
    {
        return $"{this.Cep} - {this.Uf} - {this.Bairro} - {this.Localidade}";
    }
}
