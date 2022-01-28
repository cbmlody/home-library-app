﻿using Contracts;
using Contracts.Repositories;

using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly LibraryContext _libraryContext;
        private readonly IAuthorRepository _author;
        private readonly IBookRepository _book;
        private readonly IBookSeriesRepository _bookSeries;
        private readonly IBookshelveRepository _bookshelve;
        private readonly IGenreRepository _genre;
        private readonly IPublisherRepository _publisher;

        public IAuthorRepository Author => _author ?? new AuthorRepository(_libraryContext);
        public IBookRepository Book => _book ?? new BookRepository(_libraryContext);
        public IBookSeriesRepository BookSeries => _bookSeries ?? new BookSeriesRepository(_libraryContext);
        public IBookshelveRepository Bookshelve => _bookshelve ?? new BookshelveRepository(_libraryContext);
        public IGenreRepository Genre => _genre ?? new GenreRepository(_libraryContext);
        public IPublisherRepository Publisher => _publisher ?? new PublisherReposiory(_libraryContext);

        public RepositoryWrapper(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public void Save()
        {
            _libraryContext.SaveChanges();
        }
    }
}
