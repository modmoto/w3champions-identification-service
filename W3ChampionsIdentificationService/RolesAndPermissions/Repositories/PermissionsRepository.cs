﻿using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using W3ChampionsIdentificationService.Config;
using W3ChampionsIdentificationService.RolesAndPermissions.Contracts;

namespace W3ChampionsIdentificationService.RolesAndPermissions.Repositories;

public class PermissionsRepository(MongoClient mongoClient, IAppConfig appConfig) : MongoDbRepositoryBase(mongoClient, appConfig), IPermissionsRepository
{
    public async Task<List<Permission>> GetAllPermissions(int? limit = null, int? offset = null)
    {
        return await LoadAll<Permission>(null, limit, offset);
    }

    public async Task<Permission> GetPermission(string id)
    {
        return await LoadFirst<Permission>(x => x.Id == id);
    }

    public async Task CreatePermission(Permission permission)
    {
        await Insert(permission);
    }

    public async Task UpdatePermission(Permission permission)
    {
        await Upsert(permission);
    }

    public async Task DeletePermission(string id)
    {
        await Delete<Permission>(id);
    }

    public async Task<List<string>> GetPermissionsForAdmin(string id)
    {
        var permission = await LoadFirst<Permission>(x => x.Id == id);
        if (permission != null)
        {
            return permission.Permissions.Select(permission => permission.ToString()).ToList();
        }
        return null;
    }
}
