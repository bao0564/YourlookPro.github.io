﻿using Data.Models;

namespace yourlook.MenuKid
{
    public class Page1 : IPage1
    {
        private readonly YourlookContext _context;
        public Page1(YourlookContext context)
        {
            _context = context;
        }
        public DbSanPham add(DbSanPham dbSanPham)
        {
            _context.DbSanPhams.Add(dbSanPham);
            _context.SaveChanges();
            return dbSanPham;
        }

        public DbSanPham delete(string maSP)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DbSanPham> GetAllSanPhamPage1()
        {
            return _context.DbSanPhams;
        }

        public DbSanPham GetSanPham(string maSP)
        {
            return _context.DbSanPhams.Find(maSP);
        }

        public DbSanPham update(DbSanPham dbSanPham)
        {
            _context.Update(dbSanPham);
            _context.SaveChanges();
            return dbSanPham;
        }
    }
}
