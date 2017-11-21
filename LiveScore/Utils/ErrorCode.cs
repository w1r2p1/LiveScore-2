using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScore.Utils
{
    public enum ErrorCode
    {
        NoError = 0,
        Unknown = 1,
        InvalidGroupId = 2,
        MissingScores = 3,
        InvalidScores = 4
    }
}
