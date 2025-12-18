using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Enums
{
    public enum ErrorCode
    {
        NONE = 0,
        // SYSTEM
        INTERNAL_ERROR = 1000,

        // VALIDATION
        INVALID_DATA = 2000,

        // AUTH
        TOKEN_EXPIRED = 3000,
        INVALID_CREDENTIALS = 3001,
        AUTHORIZATION_FAILED = 3002,

        // PRODUCT
        PRODUCT_NOT_FOUND = 4000,

        //USER
        USER_NOT_FOUND = 5000,
        USER_ALREADY_EXISTS = 4001,
        //ROLE
        ROLE_NOT_FOUND = 6000,
    }
}
