﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(AppDbContext context) : ControllerBase
{
    // TODO: autenticação admin
    [HttpGet]
    public ActionResult<IEnumerable<object>> GetAll()
    {
        var users = context.Users.Select(x => new { x.Id, x.CreatedAt }).ToList();
        return Ok(users);
    }

    // TODO: autenticação admin
    [HttpGet("{id}")]
    public ActionResult<object> GetById(Guid id)
    {
        var user = context.Users.SingleOrDefault(x => x.Id == id);

        if (user is null)
        {
            return NotFound("User not found.");
        }

        return Ok(new
        {
            user.Name,
            user.Email,
            user.CreatedAt,
            user.UpdatedAt
        });
    }

    // TODO: autenticação admin
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (user is null)
        {
            return BadRequest();
        }

        var existsUserWithEmail = context.Users.Any(x => x.Email == user.Email);

        if (existsUserWithEmail)
        {
            return Conflict("There is already an user using this email.");
        }

        context.Users.Add(user);
        context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, new { user.Id });
    }

    // TODO: autenticação admin
    [HttpPut("{id}")]
    public ActionResult Update(Guid id, User userData)
    {
        if (userData is null)
        {
            return BadRequest();
        }

        var user = context.Users.SingleOrDefault(x => x.Id == id);

        if (user is null)
        {
            return NotFound("User not found.");
        }

        user.Update(userData);
        context.SaveChanges();

        // trocar pra createdaction com um getMe
        return NoContent();
    }
}
