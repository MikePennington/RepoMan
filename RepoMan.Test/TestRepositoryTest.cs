﻿using Xunit;
using Rhino.Mocks;
using System.Linq;
using System.Collections.Generic;
using RepoMan.Test.Data;
using System.Data.Entity;

namespace RepoMan.Test
{
    class TestRepositoryTest
    {
        private TestRepository<RepoTestDatabaseEntities> _subject;

        public TestRepositoryTest()
        {
            _subject = new TestRepository<RepoTestDatabaseEntities>();
        }

        [Fact]
        public void where_should_return_an_iqueryable_list_of_TRepository()
        {
            _subject.InitializeRepository<Person>();
            Assert.IsType(typeof(EnumerableQuery<Person>), _subject.Where<Person>(person => true));
        }

        [Fact]
        public void save_should_add_a_TRepository_to_the_list_correct_repository()
        {
            _subject.Save(new Person() { FirstName = "Britton" });
            var people = _subject.Where<Person>(person => true);

            Assert.Equal(1, people.Count());
        }

        [Fact]
        public void where_should_return_an_iqueryable_list_of_TRepository_but_only_those_that_match_the_query_provided()
        {
            _subject.Save(new Person() { FirstName = "Britton" });
            _subject.Save(new Person() { FirstName = "Cassie" });
            var people = _subject.Where<Person>(person => person.FirstName == "Cassie");

            Assert.Equal(1, people.Count());
            Assert.Equal("Cassie", people.First().FirstName);
        }
    }
}
