<template>
  <div>
    <div class="flex justify-center mb-4">
      <div class="h-12 bg-gray-200 flex items-center justify-between rounded-full px-4 w-full max-w-md">
        <button class="flex-1 h-full flex items-center justify-center bg-green-500 rounded-l-md px-6 focus:outline-none text-white uppercase font-semibold text-sm tracking-wide"
                :class="{ 'bg-dark-green-500': activeButton === 'participant', 'text-gray-700': activeButton !== 'participant' }"
                @click="toggleButton('participant')"
                >
          Поиск участника
        </button>

        <button class="flex-1 h-full flex items-center justify-center bg-green-500 rounded-r-md px-6 focus:outline-none text-white uppercase font-semibold text-sm tracking-wide"
                :class="{ 'bg-dark-green-500': activeButton === 'team', 'text-gray-700': activeButton !== 'team' }"
                @click="toggleButton('team')">
          Поиск команды
        </button>
      </div>
    </div>

    <main>
      <component :is="currentComponent"></component>
    </main>
  </div>
</template>

<script>
import Person from './FindPerson.vue';
import Team from './FindTeam.vue';

export default {
  data() {
    return {
      activeButton: 'participant',
      currentComponent: Person
    };
  },
  methods: {
    toggleButton(button) {
      if (this.activeButton !== button) {
        this.activeButton = button;
        if (button === 'participant') {
          this.currentComponent = Person;
        } else if (button === 'team') {
          this.currentComponent = Team;
        }
      }
    },
  },
};
</script>

<style scoped>
@tailwind base;
@tailwind components;
@tailwind utilities;

.container {
    overflow-y: auto;
    max-height: 70vh;
}

.bg-green-500 {
    background-color: #3C763D;
}

.bg-dark-green-500 {
    background-color: #1D472A;
}
</style>
