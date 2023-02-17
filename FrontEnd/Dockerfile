# Stage 1: Compile and Build angular codebase

# Use official node image as the base image
FROM node:18 as build

# Set the working directory
WORKDIR /usr/local/app

# Add the source code to app
COPY --chown=node:node ./ /usr/local/app/

# Install all the dependencies
RUN npm install

# Generate the build of the application
RUN npm run build --prod

# Expose port 4200
EXPOSE 4200

ENV HOST=0.0.0.0

CMD ["npm", "run", "start"]