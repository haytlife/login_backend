# Contributing to LoginProject

Thank you for your interest in contributing to LoginProject! This document provides guidelines and instructions for contributing to this project.

## Code of Conduct

By participating in this project, you are expected to uphold our code of conduct:
- Be respectful and inclusive
- Focus on constructive feedback
- Help others learn and grow
- Maintain professional communication

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Node.js 18+ and npm
- Git
- Code editor (VS Code, Visual Studio, etc.)

### Setting Up Development Environment

1. Fork the repository
2. Clone your fork:
   ```bash
   git clone https://github.com/yourusername/login_backend.git
   cd login_backend
   ```

3. Set up backend:
   ```bash
   cd login_backend/LoginProject.API
   dotnet restore
   dotnet ef database update
   ```

4. Set up frontend:
   ```bash
   cd vite-project
   npm install
   ```

5. Create feature branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```

## Development Guidelines

### Backend (.NET)
- Follow C# coding conventions
- Use Clean Architecture principles
- Write unit tests for new features
- Ensure all API endpoints are documented
- Follow RESTful API design principles
- Use proper error handling and logging

### Frontend (React)
- Follow React best practices
- Use TypeScript when possible
- Maintain responsive design
- Follow accessibility guidelines
- Write component tests
- Use semantic HTML elements

### Code Style
- Use the provided .editorconfig
- Follow naming conventions
- Keep functions small and focused
- Write clear, descriptive variable names
- Add comments for complex logic

## Making Changes

### Before You Start
1. Check existing issues and pull requests
2. Create an issue to discuss significant changes
3. Ensure your change fits the project's scope

### Development Process
1. Create a feature branch from main
2. Make your changes
3. Add or update tests
4. Update documentation if needed
5. Ensure all tests pass
6. Commit with descriptive messages

### Commit Message Format
Use conventional commits format:
```
type(scope): description

[optional body]

[optional footer]
```

Types:
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes
- `refactor`: Code refactoring
- `test`: Adding tests
- `chore`: Maintenance tasks

Example:
```
feat(auth): add password reset functionality

- Add password reset endpoint
- Implement email notification
- Add reset token validation

Closes #123
```

## Testing

### Backend Testing
```bash
cd login_backend
dotnet test
```

### Frontend Testing
```bash
cd vite-project
npm test
```

### Manual Testing
1. Start both backend and frontend
2. Test all authentication flows
3. Verify API endpoints work correctly
4. Check responsive design on different devices

## Pull Request Process

1. Update README.md if needed
2. Update CHANGELOG.md with your changes
3. Ensure CI/CD checks pass
4. Request review from maintainers
5. Address feedback promptly

### Pull Request Template
- [ ] Code follows project conventions
- [ ] Tests added/updated
- [ ] Documentation updated
- [ ] CHANGELOG.md updated
- [ ] No breaking changes (or clearly documented)

## Reporting Issues

### Bug Reports
Include:
- Clear description of the issue
- Steps to reproduce
- Expected vs actual behavior
- Environment details (OS, browser, etc.)
- Screenshots if applicable

### Feature Requests
Include:
- Clear description of the feature
- Use case and motivation
- Proposed implementation approach
- Any relevant examples

## Development Tips

### Useful Commands
```bash
# Backend
dotnet watch run          # Hot reload development
dotnet ef migrations add  # Add database migration
dotnet test              # Run tests

# Frontend
npm run dev              # Development server
npm run build            # Production build
npm run preview          # Preview production build
```

### Debugging
- Use breakpoints in your IDE
- Check browser developer tools
- Monitor API responses
- Review application logs

## Recognition

Contributors will be:
- Listed in the project README
- Mentioned in release notes
- Credited in CHANGELOG.md

## Questions?

- Create an issue for technical questions
- Check existing documentation first
- Be specific and provide context

Thank you for contributing to LoginProject!
