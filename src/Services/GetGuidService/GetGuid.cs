using System;
using src.Services.GetGuidService;
using src.Services.RandomGuidGeneratorService;

namespace src.Services.GetGuidService
{
    public class GetGuid : IGetGuid
    {
        private readonly IRandomGuidGenerator _guidGenerator;
        public GetGuid(IRandomGuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator ?? throw new ArgumentNullException(nameof(guidGenerator));
        }
        public void GetGuidFromService()
        {
            Console.WriteLine($"hello {_guidGenerator.GetName()} your guid is {_guidGenerator.ClassGuid()}");
        }
    }
}