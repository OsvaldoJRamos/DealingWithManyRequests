using System.Text.Json;

public class ViaCepService
{
    public CepModel GetCep(string cep)
    {
        var client = new HttpClient();
        var response = client.GetAsync($"https://viacep.com.br/ws/{cep}/json/").Result;
        var content = response.Content.ReadAsStringAsync().Result;
        var cepResult = JsonSerializer.Deserialize<CepModel>(content);

        return cepResult;
    }

    public async Task<CepModel> GetCepAsync(string cep)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        var content = await response.Content.ReadAsStringAsync();
        var cepResult = JsonSerializer.Deserialize<CepModel>(content);

        return cepResult;
    }
}
