using Microsoft.AspNetCore.Mvc;
using SailMapper.Services;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/sync")]
    public class SyncController
    {
        private readonly SyncService syncService;

        public SyncController()
        {
            syncService = new SyncService();
        }
        
    }
}
