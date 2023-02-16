# Base image
FROM node:12.16.1-alpine3.9

# Set working directory
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package*.json ./

# Install npm packages
RUN npm install

# Copy project files to the working directory
COPY . .

# Build the project
RUN npm run build

# Expose port 80
EXPOSE 80

# Start the application
CMD ["npm", "run", "start"]
