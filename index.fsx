module MoneyModule =
    open System

    type Currency =
        | BRL
        | BTC

    type Money =
        { Amount: decimal
          Currency: Currency
          ValidUntil: DateTime }

        member this.IsValid = this.ValidUntil > DateTime.Now

        static member (+)(left: Money, right: decimal) =
            { Amount = left.Amount + right
              Currency = left.Currency
              ValidUntil = left.ValidUntil }
        static member (+)(left: Money, right: int32) = left + (Convert.ToDecimal(right) / 100m)

        static member (-)(left: Money, right: decimal) = left + -right
        static member (-)(left: Money, right: int32) = left - (Convert.ToDecimal(right) / 100m)

        static member (+)(left: Money, right: Money) = left + right.Amount
        static member (-)(left: Money, right: Money) = left - right.Amount

        member this.WithCurrency = this.Currency.ToString() + " " + this.Amount.ToString()
        member this.InCents = Convert.ToInt32(this.Amount * 100m)

// Main
open System
open MoneyModule

let anyAmount: Money =
    { Amount = 10.00m
      Currency = BRL
      ValidUntil = DateTime.Now }

let a = anyAmount + -10
let x = a.WithCurrency

printfn "%s" x
printfn "%s" (a.ValidUntil.ToString())
printfn "%s" (a.IsValid.ToString())
