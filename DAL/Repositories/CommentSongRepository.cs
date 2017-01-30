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
    public class CommentSongRepository : ICommentSongRepository
    {
        private readonly DbContext context;

        public CommentSongRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalCommentSong> GetAll()
        {
            //return context.Set<CommentSong>().Select(CommentSong => CommentSong.ToDalCommentSong());
            return context.Set<CommentSong>().Select(CommentSong => new DalCommentSong
            {
                Id = CommentSong.Id,
                UserId = CommentSong.UserId ?? 0,
                SongId = CommentSong.SongId ?? 0,
                Text = CommentSong.Text,
                DateAdded = CommentSong.DateAdded
            });
        }


        public DalCommentSong GetById(int key)
        {
            var ormCommentSong = context.Set<CommentSong>().FirstOrDefault(CommentSong => CommentSong.Id == key);
            return ormCommentSong.ToDalCommentSong();
        }

        public DalCommentSong GetByPredicate(Expression<Func<DalCommentSong, bool>> f)
        {
            //Expression<Func<DalCommentSong, bool>> -> Expression<Func<CommentSong, bool>> (!)
            throw new NotImplementedException();
        }

        public int Create(DalCommentSong e)
        {
            var CommentSong = e.ToOrmCommentSong();

            CommentSong.User = context.Set<User>().FirstOrDefault(m => m.Id == CommentSong.UserId);
            CommentSong.Song = context.Set<Song>().FirstOrDefault(m => m.Id == CommentSong.SongId);

            context.Set<CommentSong>().Add(CommentSong);

            return CommentSong.Id;
        }

        public void Delete(DalCommentSong e)
        {
            var CommentSong = context.Set<CommentSong>().Single(u => u.Id == e.Id);

            context.Set<CommentSong>().Remove(CommentSong);
        }

        public void Update(DalCommentSong e)
        {
            var CommentSong = context.Set<CommentSong>().First(r => (r.UserId == e.UserId && r.SongId == e.SongId));

            if (CommentSong != null)
            {
                CommentSong.Text = e.Text;

                CommentSong.Song = context.Set<Song>().First(r => (r.Id == e.SongId));

                context.Entry(CommentSong).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
}