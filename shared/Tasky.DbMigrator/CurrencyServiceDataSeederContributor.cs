using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasky.CurrencyService.Currencies;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Tasky.DbMigrator;
public class CurrencyServiceDataSeederContributor
      : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrentUser _currentUser;


    public CurrencyServiceDataSeederContributor(

        IGuidGenerator guidGenerator,
        
        ICurrencyRepository currencyRepository,
        ICurrentUser currentUser
    )
    {
        _guidGenerator = guidGenerator;
        _currencyRepository = currencyRepository;
        _currentUser = currentUser;
    }
    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        await SeedCurrenciesAsync();
     

    }

    private async Task SeedCurrenciesAsync()
    {
        if (await _currencyRepository.GetCountAsync() > 0)
        {
            return;
        }


        await _currencyRepository.InsertAsync(
            new Currency
            (
              _guidGenerator.Create(),
               "United Kingdom Pound",
                "GBP",
               "£"

            ),
            autoSave: true
        );


        await _currencyRepository.InsertAsync(

        new Currency
       (
              _guidGenerator.Create(),
           "United States Dollar",
           "USD",
            "$"

       )
        );

        await _currencyRepository.InsertAsync(
        new Currency
        (
              _guidGenerator.Create(),
            "Syrian Pound",
          "SYP",
            "£"

        ), autoSave: true
        );

        await _currencyRepository.InsertAsync(
        new Currency(
            _guidGenerator.Create(),
            "Russia Ruble",
             "RUB",
             "₽"
             ), autoSave: true





       );

    }
}

