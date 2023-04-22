using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Model.Dtos
{
    [Serializable]
    public class KantorDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IdentificationKey { get; set; }

        public KantorDto()
        {
            Name = string.Empty;
            IdentificationKey = string.Empty;
            Id = 0;
        }

        public KantorDto(Kantor kantor)
        {
            Id = kantor.Id;
            Name = kantor.Name;
            IdentificationKey = kantor.IdentificationKey;
        }

        public static List<KantorDto> Map(IEnumerable<Kantor> kantor)  => kantor.Select(x => new KantorDto(x)).ToList();

        public Kantor ConvertToEntity()
        {
            var kantor = new Kantor();
            if (Id > 0)
                kantor.Id = Id;
            kantor.Name = Name;
            kantor.IdentificationKey = IdentificationKey;
            return kantor;
        }
    }
}
