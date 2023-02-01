module MoneyModule =
    open System

    type Currency =
        | BRL
        | BTC

    type Credit =
        { Amount: decimal
          Currency: Currency
          ValidUntil: DateTime }

        member this.IsValid = this.ValidUntil > DateTime.Now

        static member (+)(left: Credit, right: decimal) =
            { Amount = left.Amount + right
              Currency = left.Currency
              ValidUntil = left.ValidUntil }

        static member (+)(left: Credit, right: int32) =
            left + (Convert.ToDecimal(right) / 100m)

        static member (-)(left: Credit, right: decimal) = left + -right

        static member (-)(left: Credit, right: int32) =
            left - (Convert.ToDecimal(right) / 100m)

        static member (+)(left: Credit, right: Credit) = left + right.Amount
        // static member (-)(left: Money, right: Money) = left - right.Amount // existe dinheiro negativo?

        member this.WithCurrency = this.Currency.ToString() + " " + this.Amount.ToString()
        member this.InCents = Convert.ToInt32(this.Amount * 100m)

module AccountModule =
    open MoneyModule

    type Account =
        { Credits: Credit[] }

        member this.Total =
            this.Credits
            |> Array.filter (fun (m: Credit) -> m.IsValid)
            |> Array.sumBy (fun (m: Credit) -> m.Amount)

// Main
open System
open MoneyModule
open AccountModule

let amount1: Credit =
    { Amount = 10.00m
      Currency = BRL
      ValidUntil = DateTime.Now.AddDays(1.0) }

let amount2: Credit =
    { Amount = 0.25m
      Currency = BRL
      ValidUntil = DateTime.Now.AddDays(2.0) }

let account: Account = { Credits = [| amount1; amount2 |] }

account.Total.ToString() |> printfn "%s"
