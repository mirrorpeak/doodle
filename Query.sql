select
	A."AccountId" "Account_AccountId",
	A."Name" "Account_Name",
	Q."QuoteId" "QuoteId",
    Q."CustomerId" "CustomerId"

from public."Quotes" Q
inner join public."Accounts" A
on A."AccountId" = Q."CustomerId"