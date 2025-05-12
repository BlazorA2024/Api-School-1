using AutoGenerator;
using AutoGenerator.Conditions;
using AutoGenerator.Notifications;
using AutoMapper;
using ApiSchool.Data;
using System;

namespace V1.Validators.Conditions
{
    public class TFactoryInjector : TBaseFactoryInjector, ITFactoryInjector
    {
        private readonly DataContext _context;
        public TFactoryInjector(IMapper mapper, IAutoNotifier notifier, DataContext context) : base(mapper, notifier)
        {
            _context = context;
        }

        public DataContext Context => _context;
    // يمكنك حقن اي طبقة
    }
}