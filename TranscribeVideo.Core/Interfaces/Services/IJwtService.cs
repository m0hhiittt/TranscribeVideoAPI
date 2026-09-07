using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(Users user);

        string GenerateRefreshToken();
    }
}
