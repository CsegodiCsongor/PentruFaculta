from random import randint
from random import random
import NeuralNet
import numpy as np
import pygame

screen_width = 600
screen_height = 600

pygame.init()
win = pygame.display.set_mode((screen_width, screen_height))
pygame.display.set_caption("MyDinoRunner")

score = 0
font = pygame.font.SysFont("comicsans", 30, True)

random_species_num = 5
best_keep_num = 3
population_cut_num = 50
initial_population_count = 100
max_draw_dino = 1
ground_height = 500
base_speed = 5
current_speed = 5
max_obstacles = 3
generation = 0
best_fitness = 0

dino_height = 80
dino_x = 50

text = font.render("Score: " + str(score) + "\n" + "Best yet: " + str(best_fitness) + "Generation: " + str(generation), 1, (0, 0, 0))


class Dino:
    def __init__(self):
        self.x = dino_x
        self.height = dino_height
        self.y = screen_height - self.height
        self.width = 50
        self.speed = 10
        self.jump_counter = 10
        self.duck = False
        self.is_jumping = False
        self.is_ducking = False
        self.brain = NeuralNet.NeuralNetwork([5, 3, 3, 3, 3, 2])
        self.fitness = 0
        self.is_dead = False

        self.jump_counter = 10

    def get_position(self):
        return self.x, self.y, self.width, self.height

    def draw(self):
        pygame.draw.rect(win, (255, 0, 0), self.get_position())

    def execute_actions(self):
        if self.duck:
            if self.is_ducking is False:
                self.height /= 2
                self.y += self.height
                self.is_ducking = True
        elif self.is_ducking is True:
            self.y -= self.height
            self.height *= 2
            self.is_ducking = False
        if self.is_jumping:
            if self.jump_counter >= -10:
                self.y -= (self.jump_counter * abs(self.jump_counter)) * 0.3
                self.jump_counter -= 0.5
            else:
                self.jump_counter = 10
                self.is_jumping = False

    def __lt__(self, other):
        if self.fitness < other.fitness:
            return True


class Obstacle:
    def __init__(self):
        if randint(1, 10) <= 5:
            self.make_cactus()
            self.is_bird = False
        else:
            self.make_bird()
            self.is_bird = True

    def update(self):
        self.x -= current_speed
        self.draw()

    def draw(self):
        pygame.draw.rect(win, (0, 255, 0), self.get_position())

    def get_position(self):
        return self.x, self.y, self.width, self.height

    def make_bird(self):
        self.height = randint(10, 20)
        self.width = randint(self.height, 40)
        self.x = screen_width
        self.y = randint(screen_height/2 + 50, screen_height - self.height - dino_height/2 - 10)

    def make_cactus(self):
        self.width = randint(35, 50)
        self.height = randint(30, 70)
        self.x = screen_width
        self.y = screen_height - self.height

    def get_stats_for_prediction(self):
        obstacle_x = self.x
        obstacle_y = 0
        if self.is_bird is False:
            obstacle_y = self.y
        obstacle_width = self.width
        bird_y = 0
        if self.is_bird is True:
            bird_y = self.y
        return [[obstacle_x, obstacle_width, obstacle_y, bird_y, current_speed]]
        # return [[self.x, self.y, self.width, self.is_bird, current_speed]]


dino_list = list()
obstacles = list()
run = True


def obstacles_controller():
    for obstacle in obstacles:
        if obstacle.x + obstacle.width < 0:
            obstacles.remove(obstacle)
        else:
            obstacle.update()


def obstacle_spawner():
    if len(obstacles) < max_obstacles and randint(0, 100) > 95:
        if len(obstacles) >= 1:
            if screen_width - obstacles[-1].x > 300:
                obstacles.append(Obstacle())
        else:
            obstacles.append(Obstacle())


def check_for_collision():
    for dino in dino_list:
        if dino.is_dead is False:
            for obstacle in obstacles:
                distance_x = abs(dino.x + dino.width/2 - (obstacle.x + obstacle.width/2))
                distance_y = abs(dino.y + dino.height/2 - (obstacle.y + obstacle.height/2))

                if distance_x <= dino.width/2 + obstacle.width/2 - 5 and distance_y <= dino.height/2 + obstacle.height/2:
                    dino.fitness = score
                    global best_fitness
                    if best_fitness < score:
                        best_fitness = score
                    dino.is_dead = True


def draw_all():
    win.fill((255, 255, 255))
    drawn_dino_count = 0
    for dino in dino_list:
        if drawn_dino_count >= max_draw_dino:
            break
        if dino.is_dead is False:
            dino.draw()
            drawn_dino_count += 1
    for obstacle in obstacles:
        obstacle.draw()

    text = font.render("Score: " + str(score) + " Best yet: " + str(best_fitness) + " Generation: " + str(generation), 1, (0, 0, 0))
    win.blit(text, (50, 10))
    pygame.display.update()


frames = 0


# region genetic_algorithm

def create_initial_population():
    for i in range(initial_population_count):
        dino_list.append(Dino())


def get_roulette_dino():
    max_fitness_value = sum([dino.fitness for dino in dino_list])
    picked_dino = randint(0, max_fitness_value - 1)
    current_fitness_value = 0
    for dino in dino_list:
        current_fitness_value += dino.fitness
        if current_fitness_value > picked_dino:
            return dino


def cut_population():
    cut_population_list = list()

    for i in range(best_keep_num):
        cut_population_list.append(dino_list[i])
        del(dino_list[i])

    for i in range(population_cut_num - best_keep_num):
        dino = get_roulette_dino()
        cut_population_list.append(dino)
        dino_list.remove(dino)
    return cut_population_list


def remake_population():
    global dino_list
    dino_list = cut_population()
    for dino in dino_list:
        dino.is_dead = False

    children_list = list()
    for n in range(initial_population_count - population_cut_num - random_species_num):
        parent_dino1 = get_roulette_dino()
        parent_dino2 = get_roulette_dino()
        while parent_dino2 == parent_dino1:
            parent_dino2 = get_roulette_dino()

        child_dino = Dino()
        for l in range(len(child_dino.brain.layers)):
            for i in range(child_dino.brain.layers[l].w.shape[0]):
                for j in range(child_dino.brain.layers[l].w.shape[1]):
                    if randint(0, 100) > 97:
                        child_dino.brain.layers[l].w[i, j] = (random() - 0.5) * 100
                    else:
                        if randint(0, 100) <= 50:
                            child_dino.brain.layers[l].w[i, j] = parent_dino1.brain.layers[l].w[i, j]
                        else:
                            child_dino.brain.layers[l].w[i, j] = parent_dino2.brain.layers[l].w[i, j]
            for i in range(child_dino.brain.layers[l].b.shape[1]):
                if randint(0, 100) > 97:
                    child_dino.brain.layers[l].b[0, i] = (random() - 0.5) * 100
                else:
                    if randint(0, 100) <= 50:
                        child_dino.brain.layers[l].b[0, i] = parent_dino1.brain.layers[l].b[0, i]
                    else:
                        child_dino.brain.layers[l].b[0, i] = parent_dino2.brain.layers[l].b[0, i]
        children_list.append(child_dino)
    for n in range(random_species_num):
        children_list.append(Dino())
    for dino in children_list:
        dino_list.append(dino)

# endregion


# region dino_controller

def dino_controller():
    decision = make_dino_decision()
    for dino in dino_list:
        if dino.is_dead is False:
            prediction = dino.brain.make_decision(decision)
            if prediction[0, 0] == 1:
                dino.is_jumping = True
            if prediction[0, 1] == 1:
                dino.duck = True
            if prediction[0, 1] == 0:
                dino.duck = False
            dino.execute_actions()


def make_dino_decision():
    k = choose_closest_obstacle()
    if k is not None:
        closest_obstacle1 = obstacles[k]
        '''
        closest_obstacle2 = None
        if k + 1 < len(obstacles):
            closest_obstacle2 = obstacles[k + 1]
        '''
        s = np.array(closest_obstacle1.get_stats_for_prediction())
        decision = np.empty((1, s.shape[1]))
        for i in range(s.shape[1]):
            decision[0, i] = s[0, i]
            '''
        if closest_obstacle2 is not None:
            decision[0, decision.shape[1] - 1] = closest_obstacle2.x - closest_obstacle1.x
        else:
            decision[0, decision.shape[1] - 1] = 0
            '''
        return decision
    else:
        return np.ones((1, dino_list[0].brain.layers[0].a.shape[1]))


def alive_dino_num():
    alive_dino_count = 0
    for dino in dino_list:
        if dino.is_dead is False:
            alive_dino_count += 1
    return alive_dino_count


def choose_closest_obstacle():
    for i in range(len(obstacles)):
        if obstacles[i].x > dino_x:
            return i
    return None

# endregion


def game_loop():
    check_for_collision()
    obstacle_spawner()
    dino_controller()
    obstacles_controller()
    draw_all()


create_initial_population()
while run:
    pygame.time.delay(10)

    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            run = False

    if alive_dino_num() > 0:
        game_loop()

        if frames == 10:
            score += 1
            frames = 0
            if score != 0 and score % 100 == 0:
                current_speed += 1
        frames += 1
    else:
        generation += 1
        dino_list.sort(reverse=True)
        obstacles.clear()
        remake_population()
        print(len(dino_list))
        frames = 0
        score = 0
        current_speed = base_speed


pygame.quit()
