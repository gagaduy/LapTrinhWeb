# BookDB CRUD Design

## Muc tieu

Chuyen du an MVC hien tai thanh bai ASM5 CRUD cho `BookDB`, bam sat cac hinh mau ma khong giu lai cau truc `Grade/Student` cu.

## Cau truc du lieu

### Category

- `CategoryId`
- `CategoryName`

### Book

- `Id`
- `Title`
- `Author`
- `Price`
- `Description`
- `Image`
- `CategoryId`

## Pham vi thay doi

- Xoa hoac thay the toan bo model, controller, view, migration, seed du lieu cu khong con phu hop.
- Tao lai `ApplicationDbContext` theo 2 bang `Book` va `Category`.
- Tao du lieu mau cho category va book theo dung dinh huong bai mau.
- Luu anh sach trong `wwwroot/images/books`.

## Chuc nang

### Trang danh sach sach

- Hien cot trai la danh sach chu de.
- Moi chu de hien ten va so luong sach, vi du: `Cuoc song (2)`.
- Cot phai hien danh sach card sach.
- Co nut `Them moi`.
- Tren moi sach co cac thao tac:
  - `Chi tiet`
  - `Sua`
  - `Xoa`

### Trang chi tiet sach

- Hien anh sach lon.
- Hien thong tin:
  - Ten sach
  - Tac gia
  - Mo ta
  - Gia
- Bo cuc bam sat hinh mau chi tiet sach.

### Form them/sua sach

- Nhap dung cac truong trong bai mau:
  - Title
  - Author
  - Price
  - Description
  - Image
  - CategoryId
- Kiem tra du lieu o client va server.

## Giao dien

- Giu bo cuc va luong su dung gan voi hinh mau.
- Su dung `DESIGN.md` de nang cap mau sac, typo, nut bam, card, spacing.
- Khong them cac khu vuc/chuc nang ngoai bai mau.
- Tong the giao dien theo huong:
  - nen sang am
  - card sach mau noi bat
  - nut bam toi mau
  - khu chi tiet sach co bo cuc sach se, hien dai hon

## Dieu huong

- `Home/Index` tro thanh trang danh sach sach.
- Thanh menu toi gian, uu tien lien ket ve trang chu va cac thao tac can thiet.

## Database va migration

- Tao migration moi phan anh mo hinh `Book` va `Category`.
- Seed category va book mau trong migration hoac bang seeding trong context, uu tien cach de bai de theo doi.
- Database hien tai co the duoc lam moi de tranh xung dot voi migration cu.

## Kiem thu

- `dotnet build` phai pass.
- `dotnet ef database update` phai tao duoc schema moi.
- Chay `dotnet run` va kiem tra:
  - trang danh sach sach
  - loc/xem theo chu de
  - xem chi tiet
  - them/sua/xoa sach

