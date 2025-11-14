using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.Application.Exceptions;

public class AppException(string reason): Exception(reason);
