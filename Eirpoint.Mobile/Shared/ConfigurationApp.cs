using Eirpoint.Mobile.Datasource.Repository.Base;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Platform.Ioc.Injection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Eirpoint.Mobile.Shared.Interfaces;

namespace Eirpoint.Mobile.Shared
{
    public class ConfigurationApp : IConfiguration
    {
        private ConfigurationEntity _configEntity;

        public int Id { get { return _configEntity.IdentityId; } }
        public bool HasBasicData { get { return _configEntity.HasBasicData; } set { UpdateBasicDataValue(value); } }

        public ConfigurationApp()
        {
            //get config
            _configEntity = Injector.Resolver<IPersistenceBase<ConfigurationEntity>>().Get().Result.FirstOrDefault();

            if (_configEntity == null)
            {
                //insert new register
                Injector.Resolver<IPersistenceBase<ConfigurationEntity>>().Insert(new ConfigurationEntity() { HasBasicData = false });

                //update config
                _configEntity = Injector.Resolver<IPersistenceBase<ConfigurationEntity>>().Get().Result.FirstOrDefault();
            }
        }

        private void UpdateBasicDataValue(bool basicDataValue)
        {
            _configEntity.HasBasicData = basicDataValue;
            Injector.Resolver<IPersistenceBase<ConfigurationEntity>>().Update(_configEntity);
        }
    }
}
