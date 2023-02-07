using Microsoft.AspNetCore.Mvc;
using sippedes.Cores.Controller;
using sippedes.Cores.Entities;
using sippedes.Src.Features.WitnessSignatures.Services;

namespace sippedes.Src.Features.WitnessSignatures.Controller
{
    [Route("api/witness-signature")]
    public class WitnessSignatureController : BaseController
    {
        private readonly IWitnessSignatureService _signatureService;

        public WitnessSignatureController(IWitnessSignatureService signatureService)
        {
            _signatureService = signatureService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewWitnessSignature([FromBody] WitnessSignature request)
        {
            var createResponse = await _signatureService.CreateNewWitnessSignature(request);
            return Created("/api/witness-signature", Success(createResponse));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWitnessSignatureById(string id)
        {
            var witnessResponse = await _signatureService.GetWitnessSignatureById(id);
            return Success(witnessResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWitnessSignature([FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var witnessData = await _signatureService.GetAllWitnessSignature(page, size);
            return Success(witnessData);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWitnessSignature([FromBody] WitnessSignature request)
        {
            var updateData = await _signatureService.UpdateWitnessSignature(request);
            return Success(updateData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWitnessSignatureById(string id)
        {
            var deleteData = _signatureService.Delete(id);
            return Success(deleteData);
        }
    }
}
