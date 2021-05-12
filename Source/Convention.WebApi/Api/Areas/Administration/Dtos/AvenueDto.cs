using System;

namespace Convention.WebApi.Api.Areas.Administration.Dtos
{
    public class AvenueDto
    {
        public Guid Id { get; }
        public string Name { get; }

        public AvenueDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
