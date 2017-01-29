using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Interfacies.DTO;
using ORM;

namespace DAL.Concrete
{
    public class FileRepository : IFileRepository
    {
        private readonly DbContext context;

        public FileRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalFile> GetAll()
        {
            return context.Set<File>().Select(File => File.ToDalFile());
            //return context.Set<File>().Select(File => new DalFile
            //{
            //    Id = File.Id,
            //    Name = File.Name,
            //    Description = File.Description,
            //    AuthorId = File.Author.Id
            //});
        }


        public DalFile GetById(int key)
        {
            var ormFile = context.Set<File>().FirstOrDefault(File => File.Id == key);
            return ormFile.ToDalFile();
        }

        public DalFile GetByPredicate(Expression<Func<DalFile, bool>> f)
        {
            //Expression<Func<DalFile, bool>> -> Expression<Func<File, bool>> (!)
            throw new NotImplementedException();
        }

        public int Create(DalFile e)
        {
            var File = e.ToOrmFile();
            context.Set<File>().Add(File);

            return File.Id;
        }

        public void Delete(DalFile e)
        {
            var File = e.ToOrmFile();
            File = context.Set<File>().Single(u => u.Id == File.Id);
            context.Set<File>().Remove(File);
        }

        public void Update(DalFile entity)
        {
            throw new NotImplementedException();
        }
    }

    //public class FileRepository : IFileRepository
    //{
    //    private readonly UnitOfWork unitOfWork;

    //    public PhotoRepository(UnitOfWork unitOfWork)
    //    {
    //        this.unitOfWork = unitOfWork;
    //    }
    //    public void Create(DalPhoto e)
    //    {
    //        unitOfWork.Context.Set<Photo>().Add(e.ToOrmPhoto());
    //        unitOfWork.Commit();
    //    }

    //    public void Delete(DalPhoto e)
    //    {
    //        var userPhoto = unitOfWork.Context.Set<Photo>().FirstOrDefault(p => p.Id == e.Id);
    //        unitOfWork.Context.Set<Photo>().Attach(userPhoto);
    //        unitOfWork.Context.Set<Photo>().Remove(userPhoto);
    //        unitOfWork.Context.Entry(userPhoto).State = System.Data.Entity.EntityState.Deleted;
    //        unitOfWork.Commit();
    //    }

    //    public IEnumerable<DalPhoto> GetAll()
    //    {
    //        var photos = unitOfWork.Context.Set<Photo>().Select(u => u).ToList();
    //        return photos.Select(p => p.ToDalPhoto()).ToList();

    //    }

    //    public IEnumerable<DalPhoto> GetAllByPredicate(Expression<Func<DalPhoto, bool>> predicate)
    //    {
    //        var visitor = new PredicateExpressionVisitor<DalPhoto, Photo>(Expression.Parameter(typeof(Photo), predicate.Parameters[0].Name));
    //        var express = Expression.Lambda<Func<Photo, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
    //        var final = unitOfWork.Context.Set<Photo>().Where(express).ToList();
    //        return final.Select(p => p.ToDalPhoto()).ToList();
    //    }

    //    public DalPhoto GetById(int key)
    //    {
    //        var photo = unitOfWork.Context.Set<Photo>().Find(key);
    //        if (photo == null)
    //            return null;
    //        return photo.ToDalPhoto();
    //    }

    //    public DalPhoto GetOneByPredicate(Expression<Func<DalPhoto, bool>> predicate)
    //    {
    //        var photo = GetAllByPredicate(predicate).FirstOrDefault();
    //        return photo;
    //    }

    //    public void Update(DalPhoto e)
    //    {
    //        var photo = unitOfWork.Context.Set<Photo>().First(x => x.Id == e.Id);
    //        unitOfWork.Context.Set<Photo>().Attach(photo);
    //        photo.Data = e.Data;
    //        photo.MimeType = e.MimeType;
    //        photo.Date = e.Date;
    //        unitOfWork.Context.Entry(photo).State = System.Data.Entity.EntityState.Modified;
    //        unitOfWork.Commit();
    //    }
    //}
}