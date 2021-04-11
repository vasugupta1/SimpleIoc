using System;
using src.Services.NameService;
namespace src.Services.RandomGuidGeneratorService
{
    public class RandomGuidGenerator : IRandomGuidGenerator
    {
        private readonly Name _nameService;
        private string _classGuid;
        public RandomGuidGenerator(Name nameService)
        {
            _nameService = nameService;
            _classGuid = Guid.NewGuid().ToString();
        }
        public string ClassGuid() =>  _classGuid;
        public string GetName () => _nameService.GetName();
    }
}