namespace Finanzplanung

/// Contains the linking elements
/// and elements common to all other modules
///
module Common =
  open System

  /// The month of a year
  type Month =
    | January
    | February
    | March
    | April
    | May
    | June
    | July
    | August
    | September
    | October
    | November
    | December

  type Year = Year of int

  /// Identifies a month in a year
  type TimeStamp = Month * Year

  /// Uniquely identifies a category
  type CategoryIdentifier = CategoryIdentifier of Guid

  /// Uniquely identifies a transaction
  type TransactionIdentifier = TransactionIdentifier of Guid

  /// Uniquely identifies an account
  type AccountIdentifier = AccountIdentifier of Guid

  /// Uniquely identifies a planned transfer from one account to another
  type PlannedTransferIdentifier = PlannedTransferIdentifier of AccountIdentifier * TransactionIdentifier

  type Currency = Currency of string

  /// Money value in 1/1000th fractions
  type Money = Money of int64 * Currency

  type SimpleTransactionDetails =
    {
        TimeStamp: TimeSpan
        Amount: Money
    }

  /// Transactions can have either a
  type TransactionDetails =
    | SimpleTransaction of SimpleTransactionDetails
    | CategorizedTransaction of SimpleTransactionDetails * CategoryIdentifier

/// This modules contains all account specific logic
module ``Account handling and planning`` =
  open System
  open Common

  /// A transaction can be either an
  /// expense or an income
  type DatedTransaction =
    | Expense of TransactionDetails
    | Income of TransactionDetails

  /// A transaction can be linked to another transaction
  type LinkedTransaction = TransactionIdentifier * DatedTransaction

  /// A planned transaction is a single transaction
  /// which maybe takes place at a later point in time
  /// all simple LinkedTransaction found with the same
  /// Identifier as the Planned transaction counts as
  /// transaction which links to the Planned transaction
  ///
  /// A planned transfer is a transfer to another account
  ///
  /// An aborted transaction can only happen, if there
  /// are no previous linked transactions in the account
  ///
  /// An aborted transfer can only happen, if there
  /// are no previous linked transfers in the account
  type PlannedTransaction =
    | Planned of LinkedTransaction
    | Aborted of TransactionIdentifier
    | PlannedTransfer of PlannedTransferIdentifier * DatedTransaction
    | AbortedTransfer of PlannedTransferIdentifier

  /// # StartingBalance
  /// - The starting balance of the account
  ///
  /// # SimpleAccountTransaction
  /// - Simple transaction, not linked to anything
  ///
  /// # LinkedAccountTransaction
  /// - Simple transaction but linked via the TransactionIdentifier
  ///   to a planned transaction
  ///
  /// # Transfer
  /// - simple unplanned Transfer from one account to another account
  ///
  /// # LinkedTransfer
  /// - a planned Transfer from one account to another account
  ///   but linked to a PlannedTransfer
  type AccountTransaction =
    | StartingBalance of DatedTransaction
    | SimpleAccountTransaction of DatedTransaction
    | LinkedAccountTransaction of LinkedTransaction
    | Transfer of AccountIdentifier * DatedTransaction
    | LinkedTransfer of PlannedTransferIdentifier * DatedTransaction

  type AccountName = AccountName of string

  type Account =
    {
        Id: AccountIdentifier
        Name: AccountName
        PlannedTransactions: PlannedTransaction list
        Transactions: AccountTransaction list
    }

module BugetPlanning =
  open Common

  type Recurrence =
    | NoRecurrence
    | Monthly of int
    | Yearly

  type Transaction =
    | Expense of Money
    | Income of Money

  type PlanningEntry =
    {
      Recurrence: Recurrence
      Category: CategoryIdentifier
      PlannedTransaction: Transaction
      ValidFrom: TimeStamp
      ValidUntil: TimeStamp
    }

  type BudgetEntry =
    | LinkedBudgetEntry of AccountIdentifier * PlanningEntry
    | UnlinkedBudgetEntry of PlanningEntry

module Overview =
  open Common
  open ``Account handling and planning``
  open BugetPlanning

  type MonthlyOverview = TimeStamp * BudgetEntry list
  type YearlyOverview = MonthlyOverview list