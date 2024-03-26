# services/project_service.py

from fastapi import HTTPException
from models import Project
from sqlmodel import Session, select
from schemas.project import ProjectCreate
from database import commit_and_handle_exception, refresh_and_handle_exception


class ProjectService:

    def insert_project_db(self, project_create: ProjectCreate, session: Session):
        new_project = Project(name=project_create.name.strip(), description=project_create.description.strip())
        session.add(new_project)
        commit_and_handle_exception(session)
        refresh_and_handle_exception(session, new_project)
        return new_project
