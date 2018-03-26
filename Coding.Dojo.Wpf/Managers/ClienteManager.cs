using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Coding.Dojo.Wpf.Managers
{
    public class ClienteManager //<T> where T : class
    {
        public T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();

            if (true) // Validar se o parametro veio preenchido
                return obj;

            var format = "dd/MM/yyyy"; // your datetime format

            // Usar um Converter para não Quebrar a Data
            var dateTimeConverter = tipoDeUmDateTimeConverter;//new I###########Converter { DateTimeFormat = format };

            return JsonConvert.DeserializeObject<T>(json, tipoDeUmDateTimeConverter);
        }
    }
}