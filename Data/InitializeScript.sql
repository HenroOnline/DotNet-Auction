DECLARE @CurrentDate datetime = GETDATE()
DECLARE @CurrentUser varchar(50) = 'HENRO'

INSERT INTO AuctionItem(Date, Title, Description, MinimumPrice, VendorName, VendorStreet, VendorHouseNumber, VendorZipCode, VendorCity, VendorEmail, VendorPhoneNumber, VendorMobileNumber, CreatedDate, CreatedUser, ModifiedKind, ModifiedDate, ModifiedUser)
VALUES(@CurrentDate, 'Fiets', 'Fiets', 12.95, 'Henro Nijboer', 'ORW', '466G', '7954GD', 'Rouveen', 'henronijboer@gmail.com', '0000000000', '1234567890', @CurrentDate, @CurrentUser, 'I', @CurrentDate, @CurrentUser)

DECLARE @AuctionItemId int = SCOPE_IDENTITY()

INSERT INTO FileAttachment (Name, Extension, ContentType, SizeInBytes, CreatedDate, CreatedUser, ModifiedKind, ModifiedDate, ModifiedUser)
VALUES('Fiets', 'jpg', 'image/jpg', 200, @CurrentDate, @CurrentUser, 'I', @CurrentDate, @CurrentUser)

DECLARE @FileAttachmentId int = SCOPE_IDENTITY()

--INSERT INTO FileAttachmentAuctionItem (Sequence, FileAttachmentId, AuctionItemId, CreatedDate, CreatedUser, ModifiedKind, ModifiedDate, ModifiedUser)
--VALUES(10, @FileAttachmentId, @AuctionItemId, @CurrentDate, @CurrentUser, 'I', @CurrentDate, @CurrentUser)

INSERT INTO AuctionItemBidding (Bidding, Date, BiddingName, BiddingStreet, BiddingHouseNumber, BiddingZipCode, BiddingCity, BiddingEmail, BiddingPhoneNumber, BiddingMobileNumber, AuctionItemId, CreatedDate, CreatedUser, ModifiedKind, ModifiedDate, ModifiedUser)
VALUES(12.95, @CurrentDate, 'Roselinde', 'ORW', '466C', '7954GD', 'Rouveen', 'aa@bb.nl', '0522-291398', '06-00000000', @AuctionItemId, @CurrentDate, @CurrentUser, 'I', @CurrentDate, @CurrentUser)

INSERT INTO AuctionItemBidding (Bidding, Date, BiddingName, BiddingStreet, BiddingHouseNumber, BiddingZipCode, BiddingCity, BiddingEmail, BiddingPhoneNumber, BiddingMobileNumber, AuctionItemId, CreatedDate, CreatedUser, ModifiedKind, ModifiedDate, ModifiedUser)
VALUES(13.95, @CurrentDate, 'Jolanda', 'ORW', '466C', '7954GD', 'Rouveen', 'aa@bb.nl', '0522-291398', '06-00000000', @AuctionItemId, @CurrentDate, @CurrentUser, 'I', @CurrentDate, @CurrentUser)
