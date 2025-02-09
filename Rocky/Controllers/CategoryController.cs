﻿using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        // so this is getting an instance of the database and assiging it to the local var
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // getting Database information to display to the view 
        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Categories.OrderBy(s => s.DisplayOrder);
            return View(objList);
        }

        // GET - CREATE
        public IActionResult Create(Category obj)
        {
            obj.created = DateTime.Now;
            return View(obj);
        }

        // POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        // GET - EDIT
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }


        // GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();                                             
            }
            return View(obj);                               
        }

        // POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

                _db.Categories.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }
    }
}
