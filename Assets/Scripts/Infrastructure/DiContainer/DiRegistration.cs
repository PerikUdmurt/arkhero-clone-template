using System;

namespace ArkheroClone.Services.DI
{
    public class DiRegistration
    {
        public Func<DiContainer, object> Factory { get; set; }
        public DiRegistrationType RegistrationType { get; set; }
        public object Instance { get; set; }
    }
}